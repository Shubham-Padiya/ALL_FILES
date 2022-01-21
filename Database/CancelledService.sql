create table CancelledService(ServiceRequestID int not null foreign key(ServiceRequestID) 
			 references ServiceSchedual(ServiceRequestID) , Reason varchar(100) not null)

select * from CancelledService