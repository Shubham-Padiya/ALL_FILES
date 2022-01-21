create table AddressOfCustomer (AddressID int not null primary key identity(1,1),
			 UserID int not null foreign key(UserID) references DetailsOfUser(UserID),
			 StreetName varchar(80) not null , HouseNo int not null , City varchar(50) not null ,
			 PostalCode char(6) not null)

select * from AddressOfCustomer