create database ElectricalDevices_v8;

use ElectricalDevices_v8;

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

create table orders
(
order_id int not null primary key identity(1,1),
order_name nvarchar(50) not null,
order_date date not null,
shiping_date date not null
);

create table suppliers
(
supplier_id int not null primary key identity(1,1),
supplier_name nvarchar(50) not null
);

create table modelTypes
(
modelType_id int not null primary key identity(1,1),
modelType_name nvarchar(50) not null
);

create table DeviceModels
(
deviceModel_id int not null primary key identity(1,1),
model_name nvarchar(50) not null,
modelType_FK int foreign key(modelType_FK) references modelTypes(modelType_id),
[weight] int not null,
price money not null,
stock_balance int,
manufacturer_FK int foreign key(manufacturer_FK) references manufacturers(manufacturer_id),
supplier_FK int foreign key(supplier_FK) references suppliers(supplier_id)
);

create table devices
(
device_id int not null primary key identity(1,1),
deviceModel_FK int foreign key(deviceModel_FK) references DeviceModels(deviceModel_id),
serial_number nvarchar(50) not null,
manufacture_date date not null,
isDefected bit
);

create table deviceOrder
(
device_id int,
order_id int,
primary key(device_id,order_id), 
foreign key(device_id) references Devices(device_id),
foreign key(order_id) references orders(order_id),
amount int
);

create table rights
(
right_id int not null primary key identity(1,1),
right_name nvarchar(50) not null
);

create table users
(
user_id int not null primary key identity(1,1),
user_name nvarchar(50) not null,
user_login nvarchar(50) not null,
user_password nvarchar(50) not null,
phone nvarchar(50) not null,
personal_discount int
);

create table userRight
(
user_id int,
right_id int,
primary key(user_id,right_id), 
foreign key(user_id) references users(user_id),
foreign key(right_id) references rights(right_id)
);

--=====================================================================================================
insert into rights
values
--right_name
('View user'),
('Add user'),
('Edit user'),
('Delete user'),

('View modelType'),
('Add modelType'),
('Edit modelType'),
('Delete modelType'),

('View deviceModel'),
('Add deviceModel'),
('Edit deviceModel'),
('Delete deviceModel'),

('View device'),
('Add device'),
('Edit device'),
('Delete device'),

('View country'),
('Add country'),
('Edit country'),
('Delete country'),

('View manufacturer'),
('Add manufacturer'),
('Edit manufacturer'),
('Delete manufacturer'),

('View supplier'),
('Add supplier'),
('Edit supplier'),
('Delete supplier'),

('View order'),
('Add order'),
('Edit order'),
('Delete order');


insert into users
values
--user_name, user_login, user_password, phone, personal_discount
('Owner', 'Admin', 'Admin', '+7(111)111-11-11', 0),
('User1', 'user1', 'user1', '+7(222)222-22-22', 0);

insert into userRight
values
--user_id, right_id
(1, 1),
(1, 2), 
(1, 3), 
(1, 4), 
(1, 5),
(1, 6),
(1, 7),
(1, 8),
(1, 9),
(1, 10),
(1, 11),
(1, 12),
(1, 13),
(1, 14),
(1, 15),
(1, 16),
(1, 17),
(1, 18),
(1, 19),
(1, 20),
(1, 21),
(1, 22),
(1, 23),
(1, 24),
(1, 25),
(1, 26),
(1, 27),
(1, 28),
(1, 29),
(1, 30),
(1, 31),
(1, 32),
(2,9);--View deviceModel

--delete from userRight
--where userRight.user_id = 2;


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

insert into modelTypes
values
('Электрочайник'),
('Утюг'),
('Пылесос');

insert into DeviceModels
values
-- model_name, modelType_id, weight, price, stockBalance, manufacturer_FK, supplier
('TWK 7603', 1, 900, 2699, 2, 3, 1), --bosch
('TWK 3P420', 1, 1200, 3599, 1, 3, 1),--bosch
('AR3119', 2, 650, 1399, 3, 4, 2),--aresa
('RL-C284', 2, 1100, 1799, 2, 1, 3),--redmond
('BGBS2LB1', 3, 3500, 6999, 1, 3, 1);--bosch

insert into orders
values
--order_name, order_date,shiping_date
('ORD23-1','2023-01-21','2023-01-22'),
('ORD23-2','2023-01-23','2023-01-23');


insert into devices
values
--deviceModel_FK,serial_number,manufacture_date,isDefected
(1,'BOSCH34-7712', '2022-11-22',0),
(1,'BOSCH34-7714', '2022-11-22',0),
(2,'BOSCH35-1225', '2022-12-07',0),
(3,'ARESA11-0044', '2022-03-12',0),
(3,'ARESA11-0048', '2022-03-13',0),
(3,'ARESA11-0054', '2022-03-14',0),
(4,'RED114-0777', '2021-03-12',0),
(4,'RED114-0677', '2021-01-02',0),
(5,'BOSCH22-0716', '2022-06-23',0);


insert into deviceOrder
values
--device_id,order_id,amount
(1,1,1),--первый заказ
(2,1,1),--первый заказ
(4,2,1);--второй заказ

--=====================================================================================================

select * from countries;
select * from manufacturers;
select * from suppliers;
select * from modelTypes;
select * from DeviceModels;
select * from orders;
select * from devices;
select * from deviceOrder;
select * from users;
select * from rights;
select * from userRight;

delete from userRight
where userRight.user_id = 3;
--=====================================================================================================
--вывести все права указанного пользователя
select rights.right_id, right_name from rights
inner join userRight as UR on rights.right_id = UR.right_id 
inner join users on users.user_id = UR.user_id
where users.user_name = 'Owner';


--найти самый дорогой вид приборов
select TOP 1 model_name, modelType_name, price from DeviceModels
inner join modelTypes on modelTypes.modelType_id = DeviceModels.modelType_FK
order by price desc;

--найти приборы с ценой в заданных пределах
select model_name, modelType_name, price from DeviceModels
inner join modelTypes on modelTypes.modelType_id = DeviceModels.modelType_FK
where price >= 1000 
and price <= 3000;

--найти все приборы заданного производителя
select model_name, modelType_name, manufacturer_name from DeviceModels
inner join manufacturers on manufacturer_id = manufacturer_FK
inner join modelTypes on modelTypes.modelType_id = DeviceModels.modelType_FK
where manufacturer_name = 'Bosch';

--найти все приборы с заданной датой выпуска
select  model_name, modelType_name, serial_number, manufacture_date from devices
inner join DeviceModels on deviceModel_FK = DeviceModels.deviceModel_id
inner join modelTypes on modelTypes.modelType_id = DeviceModels.modelType_FK
where manufacture_date = '2022-06-23';

--найти все приборы, чей вес находится в заданных пределах(интервал) для заданного производителя и в целом
select model_name, modelType_name, [weight], manufacturer_name from DeviceModels
inner join modelTypes on modelTypes.modelType_id = modelType_FK
inner join manufacturers on manufacturer_id = manufacturer_FK
where [weight]>=1000
and [weight]<=4000
and manufacturer_name = 'Bosch';

--найти долю приборов проданных за определенный период от общего времени продаж
select (sum(amount)*100/(select sum(amount) as AllSold from deviceModelOrder)) from deviceModelOrder as DevOrd
inner join orders on Orders.order_id = DevOrd.order_id
where shiping_date >= '2023-01-20' and shiping_date <= '2023-01-22';

--найти самый популярный прибор(продано наибольшее количество)
select TOP 1 model_name, modelType_name, sum(amount) as NumSold from deviceModelOrder as DevOrd
inner join DeviceModels on DevOrd.deviceModel_id = DeviceModels.deviceModel_id
inner join modelTypes on modelTypes.modelType_id = DeviceModels.modelType_FK
group by model_name, modelType_name 
order by NumSold desc;

--найти долю дешевых приборов(чья стоимость меньше заданной), поступивших от заданного поставщика
select (sum(stock_balance)*100/(select sum(stock_balance) from DeviceModels
								inner join suppliers on DeviceModels.supplier_FK = suppliers.supplier_id
								where suppliers.supplier_name = 'Supplier1')) 
								as ShareOfCheapGoods
from DeviceModels
inner join suppliers on DeviceModels.supplier_FK = suppliers.supplier_id
where DeviceModels.price < 3700
and suppliers.supplier_name = 'Supplier1';

--найти количество бракованных приборов, поступивших из заданной страны для заданного поставщика
select count(isDefected) from devices
inner join DeviceModels on devices.deviceModel_FK = DeviceModels.deviceModel_id
inner join manufacturers on DeviceModels.manufacturer_FK = manufacturers.manufacturer_id
inner join suppliers on DeviceModels.supplier_FK = suppliers.supplier_id
where isDefected = 1
and manufacturers.manufacturer_name = 'Bosch'
and suppliers.supplier_name = 'Supplier1';

--найти среднюю стоимость приборов проданных за определенный промежуток времени
select avg(price) as AVGPrice from DeviceModels
inner join deviceModelOrder as DevOrd on DevOrd.deviceModel_id = DeviceModels.deviceModel_id 
inner join Orders on Orders.order_id = DevOrd.order_id
where shiping_date >= '2023-01-20' and shiping_date <= '2023-01-22'; 

--найти на складе все приборы чья стоимость выше, чем средняя стоимость приборов заданного производителя
select model_name, modelType_name, manufacturers.manufacturer_name, price from DeviceModels
inner join modelTypes on modelTypes.modelType_id = DeviceModels.modelType_FK
inner join manufacturers on DeviceModels.manufacturer_FK = manufacturers.manufacturer_id
where manufacturers.manufacturer_name = 'Bosch'
group by price, model_name, modelType_name, manufacturers.manufacturer_name
having price > (select avg(price) from DeviceModels
				inner join manufacturers on DeviceModels.manufacturer_FK = manufacturers.manufacturer_id
				where manufacturers.manufacturer_name = 'Bosch');

Select deviceModel_id, model_name, modelType_name, weight, price, stock_balance, manufacturer_name, supplier_name  from deviceModels
inner join modelTypes on modelTypes.modelType_id = DeviceModels.modelType_FK
inner join manufacturers on manufacturers.manufacturer_id = manufacturer_FK
inner join suppliers on suppliers.supplier_id = supplier_FK;

select device_id, model_name, modelType_name, serial_number, manufacture_date, isDefected, order_name from devices
inner join Orders on Orders.order_id = devices.order_FK
inner join DeviceModels on devices.deviceModel_FK = DeviceModels.deviceModel_id
inner join modelTypes on modelTypes.modelType_id = DeviceModels.modelType_FK

select device_id, model_name, modelType_name, serial_number, manufacture_date, isDefected, order_FK from devices
inner join DeviceModels on devices.deviceModel_FK = DeviceModels.deviceModel_id
inner join modelTypes on modelTypes.modelType_id = DeviceModels.modelType_FK

select manufacturer_id, manufacturer_name, country_name from manufacturers
inner join countries on country_FK = countries.country_id;