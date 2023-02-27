create database ElectricalDevices;

use ElectricalDevices;

create table country
(
country_id int not null primary key identity(1,1),
country_name nvarchar(50) not null
);

create table manufacturer
(
manufacturer_id int not null primary key identity(1,1),
manufacturer_name nvarchar(50) not null,
country_FK int foreign key(country_FK) references country(country_id)
);

create table supplier
(
supplier_id int not null primary key identity(1,1),
supplier_name nvarchar(50) not null
);

create table [Type]
(
Type_id int not null primary key identity(1,1),
Type_name nvarchar(50) not null
);

create table Model
(
model_id int not null primary key identity(1,1),
model_name nvarchar(50) not null,
type_FK int foreign key(type_FK) references [type](Type_id),
[weight] int not null,
price money not null,
stock_balance int,
reserved int,
saled int,
manufacturer_FK int foreign key(manufacturer_FK) references manufacturer(manufacturer_id),
supplier_FK int foreign key(supplier_FK) references supplier(supplier_id)
);

create table [user]
(
user_id int not null primary key identity(1,1),
user_login nvarchar(50) not null,
user_password nvarchar(50) not null,
--salt nvarchar(50) not null,
role nvarchar(50) not null
);

create table Client
(
client_id int not null primary key identity(1,1),
client_name nvarchar(50) not null,
phone nvarchar(50) not null,
personal_discount int,
user_FK int foreign key(user_FK) references [user](user_id)
);

create table [order]
(
order_id int not null primary key identity(1,1),
order_name nvarchar(50) not null,
order_date date not null,
client_FK int foreign key(client_FK) references client(client_id)
);

create table basket
(
basket_id int not null primary key identity(1,1),
basket_name nvarchar(50) not null,
client_FK int foreign key(client_FK) references client(client_id)
);

create table device
(
device_id int not null primary key identity(1,1),
Model_FK int foreign key(Model_FK) references Model(Model_id),
serial_number nvarchar(50) not null,
manufacture_date date not null,
isDefected bit,
order_FK int foreign key(order_FK) references [order](order_id),
basket_FK int foreign key(basket_FK) references basket(basket_id)
);



create table modelOrder
(
model_id int,
order_id int,
primary key(model_id,order_id), 
foreign key(model_id) references Model(model_id),
foreign key(order_id) references [order](order_id)
);

create table modelBasket
(
model_id int,
basket_id int,
primary key(model_id,basket_id), 
foreign key(model_id) references Model(model_id),
foreign key(basket_id) references basket(basket_id),
amount int,
inStock bit
);


--=====================================================================================================
insert into [user]
values
--user_login, user_password, role
('Admin', 'Admin', 'administrator');
--('Ivanov', 'pass1', 'client');

--insert into client
--values
--client_name, phone, personal_discount,user_FK
--('»ванов »ван', '+7(913)777-77-77', 0, 2);

insert into country
values
('Russian Federation'),
('Belarus'),
('Germany'),
('United Kingdom'),
('China'),
('Italy');

insert into manufacturer
values
--manufacturer_name,country_FK
('Redmond',1),
('Vitek',1),
('Bosch',3),
('Aresa',2),
('Atlant',2),
('Dyson',4),
('Kenwood',4),
('Xiaomi',5),
('AEG',6),
('Elica',6),
('Haier',5);

insert into supplier
values
('Supplier1'),
('Supplier2'),
('Supplier3'),
('Supplier4'),
('Supplier5'),
('Supplier6');

insert into [Type]
values
('Ёлектрочайник'),
('”тюг'),
('ѕылесос');

insert into model
values
-- model_name, Type_FK, weight, price, stockBalance, reserved, saled, manufacturer_FK, supplier_FK
('TWK 7603', 1, 900, 2699, 4, 0, 0, 3, 1), --bosch
('TWK 3P420', 1, 1200, 3599, 2, 0, 0, 3, 1),--bosch
('AR3119', 2, 650, 1399, 3, 0, 0, 4, 2),--aresa
('RL-C284', 2, 1100, 1799, 1, 0, 0, 1, 3),--redmond
('BGBS2LB1', 3, 3500, 6999, 1, 0, 0, 3, 1),--bosch
('TWK 7607', 1, 950, 2799, 6, 0, 0, 3, 1); --bosch

--insert into order
--values
--order_name, order_date, client_FK
--('ORDER-1','2023-01-21',1),
--('ORDER-2','2023-01-23',1);


insert into device
values
--Model_FK, serial_number, manufacture_date, isDefected, order_FK, basket_FK
(1,'BOSCH34-7712', '2022-11-22',0,null,null),
(1,'BOSCH34-7714', '2022-11-22',0,null,null),
(1,'BOSCH34-7757', '2022-12-22',0,null,null),
(1,'BOSCH34-7789', '2022-12-27',0,null,null),
(2,'BOSCH35-1225', '2022-12-07',0,null,null),
(2,'BOSCH35-1228', '2022-12-07',0,null,null),
(3,'ARESA11-0044', '2022-03-12',0,null,null),
(3,'ARESA11-0048', '2022-03-13',0,null,null),
(3,'ARESA11-0054', '2022-03-14',0,null,null),
(4,'RED114-0777', '2021-03-12',0,null,null),
(4,'RED114-0677', '2021-01-02',0,null,null),
(5,'BOSCH22-0716', '2022-06-23',0,null,null),
(6,'BOSCH38-0012', '2022-11-22',0,null,null),
(6,'BOSCH38-0014', '2022-11-22',0,null,null),
(6,'BOSCH38-0034', '2022-11-22',0,null,null),
(6,'BOSCH38-0035', '2022-11-22',0,null,null),
(6,'BOSCH38-0037', '2022-11-22',0,null,null),
(6,'BOSCH38-0044', '2022-11-22',0,null,null);

--insert into modelOrder
--values
--model_id,order_id,amount
--(1,1,2),--первый заказ
--(2,1,1),--первый заказ
--(4,2,1);--второй заказ

--insert into baskets
--values
--basket_name,client_FK
--('Ёлектротовары', 1);

--=====================================================================================================
select * from country;
select * from manufacturer;
select * from supplier;
select * from [type];
select * from model;
select * from [order];
select * from device; 
select * from modelOrder;
select * from [user];
select * from client;
select * from modelbasket;
select * from basket;

--delete from users where user_id = 2;
--delete from clients where client_id = 1;
--delete from [order] where order_id = 1;
--update modelbasket set amount = 0 where model_id = 1 and basket_id = 1;
--delete from modelBasket where model_id = 1;
--delete from baskets where basket_id = 10;

