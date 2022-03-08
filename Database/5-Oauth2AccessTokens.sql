USE BNB;

CREATE TABLE Oauth2(
    `UserId` VARCHAR(128) PRIMARY KEY,
    `AccessToken` BINARY(16) unique,
    `RefreshToken` BINARY(16) unique,
    FOREIGN KEY(`UserId`) REFERENCES UserInformation(`Email`)
);