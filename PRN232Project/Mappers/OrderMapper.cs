using BusinessObjects;
using DTOs;

namespace Mappers
{
    public static class OrderMapper
    {
        public static OrderResponseDTO ToOrderResponseDto(Order order)
        {
            return new OrderResponseDTO
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                ShippingAddress = order.ShippingAddress,
                Message = order.Message,
                OrderStatus = order.OrderStatus,
                DiscountPrice = order.DiscountPrice,
                User = new UserResponseDto
                {
                    Id = order.User.Id,
                    Username = order.User.Username,
                    Fullname = order.User.Fullname,
                    Email = order.User.Email
                },
                OrderDetails = order.OrderDetails.Select(od => ToOrderDetailResponseDto(od)).ToList()
            };
        }
        public static Order ToOrderEntity(OrderRequestDTO dto, int userId)
        {
            return new Order
            {
                UserId = userId,
                OrderDate = dto.OrderDate,
                ShippingAddress = dto.ShippingAddress,
                Message = dto.Message,
                OrderStatus = "PENDING",
                DiscountPrice = dto.DiscountPrice,
            };
        }

        public static OrderDetailResponseDTO ToOrderDetailResponseDto(OrderDetail orderDetail)
        {
            return new OrderDetailResponseDTO
            {
                Id = orderDetail.Id,
                Price = orderDetail.price,
                Quantity = orderDetail.Quantity,
                Product = new ProductResponseDTO
                {
                    Id = orderDetail.Product.Id,
                    Name = orderDetail.Product.Name,
                    Price = orderDetail.Product.Price,
                    Description = orderDetail.Product.Description
                    // Add image or category if needed
                }
            };
        }
    }
}
