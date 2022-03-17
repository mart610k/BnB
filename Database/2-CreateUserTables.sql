USE bnb; 

CREATE TABLE `userinformation`(
    `username` VARCHAR(128) PRIMARY KEY NOT NULL, 
    `email` VARCHAR(128) NOT NULL, 
    `name` VARCHAR(128) NOT NULL, 
    `created` TIMESTAMP DEFAULT NOW()
);

CREATE TABLE `usercredential`(
    `username` VARCHAR(128) PRIMARY KEY NOT NULL, 
    `password` VARCHAR(128) NOT NULL, 
    FOREIGN KEY(`username`) REFERENCES `userinformation`(`username`)
);