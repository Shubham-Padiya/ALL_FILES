create table FavoritSP(UserID int not null foreign key(UserID) references DetailsOfUser(UserID),
			 Fav_SP_ID int not null foreign key(UserID) references DetailsOfUser(UserID))

select * from FavoritSP