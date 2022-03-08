USE BNB; 

CREATE TABLE UserInformation(`Email` VARCHAR(128) PRIMARY KEY, `Name` VARCHAR(128) NOT NULL, `Created` TIMESTAMP DEFAULT NOW());

CREATE TABLE UserCredential(`Username` VARCHAR(128) PRIMARY KEY, `Password` VARCHAR(128), FOREIGN KEY(`Username`) REFERENCES UserInformation(`Username`));