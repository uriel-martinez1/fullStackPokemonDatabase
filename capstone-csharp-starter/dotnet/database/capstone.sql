USE master
GO

--drop database if it exists
IF DB_ID('pokemon_capstone') IS NOT NULL
BEGIN
	ALTER DATABASE pokemon_capstone SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE pokemon_capstone;
END

CREATE DATABASE pokemon_capstone
GO

USE pokemon_capstone
GO

--create tables
CREATE TABLE users (
	user_id int IDENTITY(1,1) NOT NULL,
	username varchar(50) NOT NULL,
	password_hash varchar(200) NOT NULL,
	salt varchar(200) NOT NULL,
	user_role varchar(50) NOT NULL
	CONSTRAINT PK_user PRIMARY KEY (user_id)
)

CREATE TABLE pokemon(
  id INT IDENTITY PRIMARY KEY,
  api_id INT NOT NULL,
  name VARCHAR(56) NOT NULL UNIQUE,
  base_experience INT,
  height INT,
  weight INT, 
  front_url VARCHAR (256),
  back_url VARCHAR(256)
);

CREATE TABLE users_pokemon (
   pokemon_id INT NOT NULL,
   users_id INT NOT NULL,
   CONSTRAINT fk_pokemon FOREIGN KEY (pokemon_id) REFERENCES pokemon(id),
   CONSTRAINT fk_users FOREIGN KEY (users_id) REFERENCES users(user_id)
);

ALTER TABLE users_pokemon
 ADD PRIMARY KEY (pokemon_id, users_id);

--populate default data
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('user','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('admin','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','admin');

GO