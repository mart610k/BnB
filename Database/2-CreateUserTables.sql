USE bnb; 

CREATE TABLE `userinformation`(
    `username` VARCHAR(128) PRIMARY KEY, 
    `email` VARCHAR(128), 
    `name` VARCHAR(128) NOT NULL, 
    `created` TIMESTAMP DEFAULT NOW()
);

CREATE TABLE `usercredential`(
    `username` VARCHAR(128) PRIMARY KEY, 
    `password` VARCHAR(128), 
    FOREIGN KEY(`username`) REFERENCES userinformation(`username`)
);