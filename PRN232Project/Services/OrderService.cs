using BusinessObjects;
using DTOs;
using Mappers;
using Repositories;

namespace Services
{
    public class OrderService
    {
        private readonly OrderRepository _orderRepo;
        private readonly ProductRepository _productRepo;
        //private readonly ProductIngredientRepository _productIngredientRepo;
        private readonly UserRepository _userRepo;
        //private readonly ProductImageRepository _productImageRepo;
        private readonly CartService _cartService;
        //private readonly CouponService _couponService;

        public OrderService(
            OrderRepository orderRepo,
            ProductRepository productRepo,
            //ProductIngredientRepository productIngredientRepo,
            UserRepository userRepo,
            //ProductImageRepository productImageRepo,
            CartService cartService
            //CouponService couponService,
            )
        {
            _orderRepo = orderRepo;
            _productRepo = productRepo;
            //_productIngredientRepo = productIngredientRepo;
            _userRepo = userRepo;
            //_productImageRepo = productImageRepo;
            _cartService = cartService;
            //_couponService = couponService;
        }

        public async Task<List<OrderResponseDTO>> GetOrdersByUserAsync(int userId)
        {
            var orders = await _orderRepo.GetAllByUserAsync(userId);
            var responses = orders.Select(OrderMapper.ToOrderResponseDto).ToList();

            //foreach (var order in responses)
            //{
            //    foreach (var detail in order.OrderDetails)
            //    {
            //        detail.Product.Image = await _productImageRepo.GetFirstImageByProductIdAsync(detail.Product.Id);
            //    }
            //}

            return responses;
        }

        public async Task<List<OrderResponseDTO>> GetAllOrdersAsync()
        {
            var orders = await _orderRepo.GetAllAsync();
            var responses = orders.Select(OrderMapper.ToOrderResponseDto).ToList();

            //foreach (var order in responses)
            //{
            //    foreach (var detail in order.OrderDetails)
            //    {
            //        detail.Product.Image = await _productImageRepo.GetFirstImageByProductIdAsync(detail.Product.Id);
            //    }
            //}

            return responses;
        }

        public async Task<OrderResponseDTO> GetOrderDetailsAsync(int orderId, int userId, bool isAdmin)
        {
            var order = await _orderRepo.GetByIdAsync(orderId) ?? throw new Exception("Order not found");

            if (!isAdmin && order.UserId != userId)
                throw new UnauthorizedAccessException("Access denied");

            var dto = OrderMapper.ToOrderResponseDto(order);
            //foreach (var detail in dto.OrderDetails)
            //{
            //    detail.Product.Image = await _productImageRepo.GetFirstImageByProductIdAsync(detail.Product.Id);
            //}

            return dto;
        }

        public async Task<List<OrderResponseDTO>> GetOrdersWithFiltersAsync(int userId, DateTime? start, DateTime? end, string? status)
        {
            var orders = await _orderRepo.GetByUserWithFilters(userId, start, end, status);
            return orders.Select(OrderMapper.ToOrderResponseDto).ToList();
        }

        public async Task<List<OrderResponseDTO>> GetOrdersWithFiltersAdminAsync(DateTime? start, DateTime? end, string? status)
        {
            var orders = await _orderRepo.GetAllWithFilters(start, end, status);
            return orders.Select(OrderMapper.ToOrderResponseDto).ToList();
        }

        public async Task PlaceOrderAsync(OrderRequestDTO request, string? couponCode, int userId)
        {
            var cartItems = await _cartService.GetAllCartItems(userId);

            var user = await _userRepo.GetByIdAsync(userId) ?? throw new Exception("User not found");

            var order = new Order
            {
                UserId = userId,
                User = user,
                OrderDate = DateTime.UtcNow,
                ShippingAddress = request.ShippingAddress,
                Message = request.Message,
                OrderStatus = "Order Received",
                DiscountPrice = request.DiscountPrice
            };

            //if (!string.IsNullOrWhiteSpace(couponCode))
            //{
            //    var coupon = await _couponService.GetByCodeAsync(couponCode);
            //    if (coupon != null)
            //    {
            //        coupon.UseCount += 1;
            //        order.Coupon = coupon;
            //    }
            //}

            var orderDetails = new List<OrderDetail>();
            foreach (var cartItem in cartItems)
            {
                // ADD LATER 

                //var product = await _productRepo.GetByIdAsync(cartItem.Product.Id) ?? throw new Exception("Product not found");

                //orderDetails.Add(new OrderDetail
                //{
                //    Order = order,
                //    Product = product,
                //    ProductId = product.Id,
                //    Price = product.Price,
                //    Quantity = cartItem.Quantity
                //});
            }

            order.OrderDetails = orderDetails;

            await _cartService.ClearCart(userId);
            await _orderRepo.SaveAsync(order);
        }

        public async Task CancelOrderAsync(int orderId)
        {
            var order = await _orderRepo.GetByIdAsync(orderId) ?? throw new Exception("Order not found");

            order.OrderStatus = "Order Canceled";
            await _orderRepo.UpdateAsync(order);

        }

        public async Task CancelOrderRequestAsync(int orderId, string reason)
        {
            var order = await _orderRepo.GetByIdAsync(orderId) ?? throw new Exception("Order not found");
            order.OrderStatus = "Cancel Pending";
            order.CancelReason = reason;
            await _orderRepo.UpdateAsync(order);
        }

        public async Task ApproveCancelAsync(int orderId)
        {
            await CancelOrderAsync(orderId);
        }

        public async Task RejectCancelAsync(int orderId)
        {
            var order = await _orderRepo.GetByIdAsync(orderId) ?? throw new Exception("Order not found");
            order.OrderStatus = "Order Received";
            await _orderRepo.UpdateAsync(order);
        }

        public async Task UpdateOrderStatusAsync(int orderId, string status)
        {
            var order = await _orderRepo.GetByIdAsync(orderId) ?? throw new Exception("Order not found");
            order.OrderStatus = status;
            await _orderRepo.UpdateAsync(order);
        }

        public async Task<int> UpdateMultipleOrderStatusAsync(string status, List<int> orderIds)
        {
            return await _orderRepo.UpdateStatusByIdsAsync(status, orderIds);
        }

        public async Task<int> CountOrdersByStatusAsync(string status)
        {
            var list = await _orderRepo.GetByStatusAsync(status);
            return list.Count;
        }

        public async Task<long[]> CountDeliveredOrdersByMonthAsync(string status, int year)
        {
            return await _orderRepo.CountOrdersByStatusAndMonthAsync(status, year);
        }
    }
}
