USE bnb;

CREATE TABLE `oauth2`(
    `username` VARCHAR(128) PRIMARY KEY NOT NULL,
    `accesstoken` BINARY(16) UNIQUE NOT NULL,
    `refreshtoken` BINARY(16) UNIQUE NOT NULL,
    `expires` TIMESTAMP NOT NULL,
    FOREIGN KEY(`username`) REFERENCES `userinformation`(`username`)
);