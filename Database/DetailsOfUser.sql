create table DetailsOfUser (UserID int not null primary key identity(1,1) , RoleID int not null 
			 foreign key(RoleID) references TypeOfRole(RoleID) , First_Name varchar(50) not null, 
			 Last_Name varchar(50) not null , Email varchar(30) not null unique , Password varchar(20) not null,
			 MobileNumber char(10) null, DateOfBirth date not null)

select * from DetailsOfUser