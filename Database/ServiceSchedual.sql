create table ServiceSchedual(ServiceRequestID int not null primary key identity(1,1),
			 CustomerID int not null foreign key(CustomerID) references DetailsOfUser(UserID),
			 ServiceDate date not null , ServiceTime time not null , Comments varchar(100) null,
			 HasPet bit not null, AcceptedBySP int null foreign key(AcceptedBySP) references DetailsOfUser(UserID),
			 PaymentAmount decimal not null , ServiceStatus varchar(10) not null)

select * from ServiceSchedual