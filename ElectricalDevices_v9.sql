create database ElectricalDevices;

use ElectricalDevices;

create table countries
(
country_id int not null primary key identity(1,1),
country_name nvarchar(50) not null
);

create table manufacturers
(
manufacturer_id int not null primary key identity(1,1),
manufacturer_name nvarchar(50) not null,
country_FK int foreign key(country_FK) references countries(country_id)
);

create table suppliers
(
supplier_id int not null primary key identity(1,1),
supplier_name nvarchar(50) not null
);

create table [Types]
(
Type_id int not null primary key identity(1,1),
Type_name nvarchar(50) not null
);

create table Models
(
model_id int not null primary key identity(1,1),
model_name nvarchar(50) not null,
type_FK int foreign key(type_FK) references [types](Type_id),
[weight] int not null,
price money not null,
stock_balance int,
manufacturer_FK int foreign key(manufacturer_FK) references manufacturers(manufacturer_id),
supplier_FK int foreign key(supplier_FK) references suppliers(supplier_id),
reserved int
);

--alter table models
--add reserved int;


create table devices
(
device_id int not null primary key identity(1,1),
Model_FK int foreign key(Model_FK) references Models(Model_id),
serial_number nvarchar(50) not null,
manufacture_date date not null,
order_FK int foreign key(order_FK) references orders(order_id),
basket_FK int foreign key(basket_FK) references baskets(basket_id),
isDefected bit
);

create table users
(
user_id int not null primary key identity(1,1),
user_login nvarchar(50) not null,
user_password nvarchar(50) not null,
--salt nvarchar(50) not null,
role nvarchar(50) not null
);

create table Clients
(
client_id int not null primary key identity(1,1),
client_name nvarchar(50) not null,
phone nvarchar(50) not null,
personal_discount int,
user_FK int foreign key(user_FK) references users(user_id)
);

create table orders
(
order_id int not null primary key identity(1,1),
order_name nvarchar(50) not null,
order_date date not null,
client_FK int foreign key(client_FK) references clients(client_id)
);

create table modelOrder
(
model_id int,
order_id int,
primary key(model_id,order_id), 
foreign key(model_id) references Models(model_id),
foreign key(order_id) references orders(order_id),
amount int
);

create table baskets
(
basket_id int not null primary key identity(1,1),
basket_name nvarchar(50) not null,
client_FK int foreign key(client_FK) references clients(client_id)
);

create table modelBasket
(
model_id int,
basket_id int,
primary key(model_id,basket_id), 
foreign key(model_id) references Models(model_id),
foreign key(basket_id) references baskets(basket_id),
amount int,
inStock bit
);


--=====================================================================================================
insert into users
values
--user_login, user_password, role
('Admin', 'Admin', 'administrator'),
('Ivanov', 'pass1', 'client');

insert into clients
values
--client_name, phone, personal_discount,user_FK
('»ванов »ван', '+7(913)777-77-77', 0, 2);

insert into countries
values
('Russian Federation'),
('Belarus'),
('Germany'),
('United Kingdom'),
('China'),
('Italy');

insert into manufacturers
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

insert into suppliers
values
('Supplier1'),
('Supplier2'),
('Supplier3'),
('Supplier4'),
('Supplier5'),
('Supplier6');

insert into [Types]
values
('Ёлектрочайник'),
('”тюг'),
('ѕылесос');

insert into models
values
-- model_name, Type_FK, weight, price, stockBalance, manufacturer_FK, supplier_FK, reserved
('TWK 7603', 1, 900, 2699, 2, 3, 1, 0), --bosch
('TWK 3P420', 1, 1200, 3599, 1, 3, 1, 0),--bosch
('AR3119', 2, 650, 1399, 3, 4, 2, 0),--aresa
('RL-C284', 2, 1100, 1799, 1, 1, 3, 0),--redmond
('BGBS2LB1', 3, 3500, 6999, 1, 3, 1, 0),--bosch
('TWK 7607', 1, 950, 2799, 5, 3, 1, 0); --bosch

insert into orders
values
--order_name, order_date, client_FK
('ORDER-1','2023-01-21',1),
('ORDER-2','2023-01-23',1);


insert into devices
values
--Model_FK, serial_number, manufacture_date, order_FK, basket_FK, isDefected
(1,'BOSCH34-7712', '2022-11-22',1,null,0),
(1,'BOSCH34-7714', '2022-11-22',1,null,0),
(1,'BOSCH34-7757', '2022-12-22',null,null,0),
(1,'BOSCH34-7789', '2022-12-27',null,null,0),
(2,'BOSCH35-1225', '2022-12-07',null,null,0),
(3,'ARESA11-0044', '2022-03-12',null,null,0),
(3,'ARESA11-0048', '2022-03-13',null,null,0),
(3,'ARESA11-0054', '2022-03-14',null,null,0),
(4,'RED114-0777', '2021-03-12',null,null,0),
(4,'RED114-0677', '2021-01-02',2,null,0),
(5,'BOSCH22-0716', '2022-06-23',null,null,0),
(6,'BOSCH38-0012', '2022-11-22',null,null,0),
(6,'BOSCH38-0014', '2022-11-22',null,null,0),
(6,'BOSCH38-0034', '2022-11-22',null,null,0),
(6,'BOSCH38-0035', '2022-11-22',null,null,0),
(6,'BOSCH38-0037', '2022-11-22',null,null,0),
(6,'BOSCH38-0044', '2022-11-22',null,null,0);

insert into modelOrder
values
--model_id,order_id,amount
(1,1,1),--первый заказ
(2,1,1),--первый заказ
(4,2,1);--второй заказ

insert into baskets
values
--basket_name,client_FK
('Ёлектротовары', 1);

--=====================================================================================================

select * from countries;
select * from manufacturers;
select * from suppliers;
select * from [types];
select * from models;
select * from orders;
select * from devices;
select * from modelOrder;
select * from users;
select * from clients;
select * from modelbasket;
select * from baskets;

--delete from users where user_id = 2;
--delete from clients where client_id = 1;
--delete from orders where order_id = 8;
--update modelbasket set amount = 0 where model_id = 1 and basket_id = 1;
--delete from modelBasket where model_id = 1;
--delete from baskets where basket_id = 10;


--=====================================================================================================

--вывод модели в алфавитном пор€дке по поставщику
select * from models
inner join suppliers on supplier_FK = supplier_id
order by supplier_name

--вывод модели в алфавитном пор€дке по поставщику по убыванию
select * from models
inner join suppliers on supplier_FK = supplier_id
order by supplier_name desc

--вывод модели в алфавитном пор€дке по производителю
select * from models
inner join manufacturers on manufacturer_FK = manufacturer_id
order by manufacturer_name

--вывод модели в алфавитном пор€дке по стране производства
select * from models
inner join manufacturers on manufacturer_FK = manufacturer_id
inner join countries on country_FK = country_id
order by country_name;

--вывод модели по наличию
select * from models
order by stock_balance desc;

--вывод модели в алфавитном пор€дке по дате продажи
select * from models
inner join devices on model_FK = model_id
inner join orders on order_FK = order_id
order by order_date;
