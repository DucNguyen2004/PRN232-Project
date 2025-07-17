USE PRN232Project
GO

/*----------------------------------------------*/

INSERT INTO roles (Name, Description) VALUES
	('admin', 'admin'),
	('user', 'user')
GO

INSERT INTO users (Username, Password, IsHavePassword, Fullname, Phone, Email, Gender, Status, IsActivated, CreatedAt, UpdatedAt) VALUES
	('test1', '12345@', 1, 'Test 1', '0123456789', 'test1@gm.co', 'Male', 'ACTIVE', 1, '2025-06-26 00:00:00.0000000', '2025-06-26 00:00:00.0000000'),
	('test2', '12345@', 1, 'Test 2', '0123456789', 'test2@gm.co', 'Female', 'ACTIVE', 1, '2025-06-26 00:00:00.0000000', '2025-06-26 00:00:00.0000000')
GO

INSERT INTO user_roles (UsersId, RolesId) VALUES
	(1, 2),
	(2, 2)
GO

/*----------------------------------------------*/

INSERT INTO categories (Name, Status) VALUES
	('Coffee', 1),
	('Milk Tea', 1),
	('Tea', 1),
	('Juice', 1)
GO

INSERT INTO products (Name, Description, Price, CategoryId, Sold, Status, PrevStatus, CreateAt) VALUES
	('Black Coffee', '...', 10000, 1, 0, 'ACTIVE', 'ACTIVE', '2025-06-26 00:00:00.0000000'),
	('Milk Coffee', '...', 15000, 1, 0, 'ACTIVE', 'ACTIVE', '2025-06-26 00:00:00.0000000'),
	('Traditional Milk Tea', '...', 25000, 2, 0, 'ACTIVE', 'ACTIVE', '2025-06-26 00:00:00.0000000'),
	('Brown Sugar Bubble Milk Tea', '...', 35000, 2, 0, 'ACTIVE', 'ACTIVE', '2025-06-26 00:00:00.0000000'),
	('Fruit Tea', '...', 20000, 3, 0, 'ACTIVE', 'ACTIVE', '2025-06-26 00:00:00.0000000'),
	('Black Tea', '...', 20000, 3, 0, 'ACTIVE', 'ACTIVE', '2025-06-26 00:00:00.0000000'),
	('Orange Juice', '...', 15000, 4, 0, 'ACTIVE', 'ACTIVE', '2025-06-26 00:00:00.0000000'),
	('Apple Juice', '...', 15000, 4, 0, 'ACTIVE', 'ACTIVE', '2025-06-26 00:00:00.0000000')
GO

INSERT INTO options (Name) VALUES
	('Size'), ('Ice'), ('Sugar'), ('Temperature')
GO

INSERT INTO option_values (Name, OptionId) VALUES
	('S', 1),
	('M', 1),
	('L', 1),
	('100%', 2),
	('50%', 2),
	('0%', 2),
	('100%', 3),
	('50%', 3),
	('30%', 3),
	('Hot', 4)
GO

INSERT INTO product_options (ProductId, OptionValueId, DeltaPrice) VALUES
	(1, 2, 0), (1, 3, 2000), (1, 4, 0), (1, 5, 0), (1, 6, 0), (1, 7, 0), (1, 8, 0), (1, 9, 0), (1, 10, 0),
	(2, 2, 0), (2, 3, 3000), (2, 4, 0), (2, 5, 0), (2, 6, 0), (2, 7, 0), (2, 8, 0), (2, 9, 0), (2, 10, 0),
	(3, 2, 0), (3, 3, 5000), (3, 4, 0), (3, 5, 0), (3, 6, 0), (3, 7, 0), (3, 8, 0), (3, 9, 0), (3, 10, 0),
	(4, 2, 0), (4, 3, 7000), (4, 4, 0), (4, 5, 0), (4, 6, 0), (4, 7, 0), (4, 8, 0), (4, 9, 0), (4, 10, 0),
	(5, 2, 0), (5, 4, 0), (5, 5, 0), (5, 6, 0), (5, 7, 0), (5, 8, 0), (5, 9, 0), (5, 10, 0),
	(6, 2, 0), (6, 4, 0), (6, 5, 0), (6, 6, 0), (6, 7, 0), (6, 8, 0), (6, 9, 0), (6, 10, 0),
	(7, 2, 0), (7, 4, 0), (7, 5, 0), (7, 6, 0), (7, 7, 0), (7, 8, 0), (7, 9, 0),
	(8, 2, 0), (8, 4, 0), (8, 5, 0), (8, 6, 0), (8, 7, 0), (8, 8, 0), (8, 9, 0)
GO