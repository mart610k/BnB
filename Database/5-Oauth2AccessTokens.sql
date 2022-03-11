USE bnb;

CREATE TABLE `oauth2`(
    `username` VARCHAR(128) PRIMARY KEY,
    `accesstoken` BINARY(16) unique,
    `refreshtoken` BINARY(16) unique,
    `expires` Timestamp,
    FOREIGN KEY(`username`) REFERENCES `userinformation`(`username`)
);