CREATE TABLE `Oauth2`(
    `UserName` VARCHAR(128) PRIMARY KEY,
    `AccessToken` BINARY(16) unique,
    `RefreshToken` BINARY(16) unique,
    `Expires` Timestamp,
    FOREIGN KEY(`UserName`) REFERENCES UserInformation(`Username`)
);