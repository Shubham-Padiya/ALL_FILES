create table BlockedCustomerBySP(CustBlockedBySPID int not null foreign key(CustBlockedBySPID) 
			 references DetailsOfUser(UserID),CustomerID int not null foreign key(CustomerID) 
			 references DetailsOfUser(UserID))

select * from BlockedCustomerBySP