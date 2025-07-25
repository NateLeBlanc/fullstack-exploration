﻿CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;
CREATE TABLE "Clients" (
    "Id" integer GENERATED BY DEFAULT AS IDENTITY,
    "Name" text NOT NULL,
    "Age" integer NOT NULL,
    CONSTRAINT "PK_Clients" PRIMARY KEY ("Id")
);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250605153155_InitialCreate', '9.0.5');

ALTER TABLE "Clients" RENAME COLUMN "Name" TO "LastName";

ALTER TABLE "Clients" ADD "FirstName" text NOT NULL DEFAULT '';

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250605160923_AddFirstAndLastNameToClient', '9.0.5');

COMMIT;

