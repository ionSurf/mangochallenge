/* Create GUIDs */
DECLARE @UNI UNIQUEIDENTIFIER
SET @UNI = NEWID()
 
SELECT @UNI

/* Create database */
CREATE DATABASE mangodb;
GO

/* Change to the database */
USE mangodb;
GO

/* Create tables */
CREATE TABLE portrait (
    PortraitId UNIQUEIDENTIFIER PRIMARY KEY default NEWID(),
    Title nvarchar(50) NOT NULL,
    Description text NOT NULL,
    URL nvarchar(150) NOT NULL,
);

CREATE TABLE users (
    UserId UNIQUEIDENTIFIER PRIMARY KEY default NEWID(),
    Username nvarchar(45) NOT NULL,
    Password nvarchar(45) NOT NULL,
);
GO

INSERT INTO portrait (PortraitId,Title,Description,URL) 
VALUES 
('021ea48b-70cb-4cc4-9982-3029a194d184','Olga Dorsey','Fit as a Fiddle','media/photo-of-man-lying-on-brown-plants-3108898.jpg'),
('147b7214-6386-428e-add1-d96b53bb925c','Rolland Hull','Happy as a Clam','media/man-denim-jacket-1801073.jpg'),
('215a4f25-0842-4359-b5fc-5458a34778c4','Harold Odom','Hit Below The Belt','media/women-s-white-and-black-button-up-collared-shirt-774909.jpg'),
('2bec292c-c82c-40e9-a01f-7cbc82ee731d','Neil Foster','Tough It Out','media/man-covering-his-face-with-floral-blanket-1862550.jpg'),
('3d8c35d1-0d11-482b-938f-ed48c720ce4b','Kerry Huffman','A Piece of Cake','media/man-in-red-jacket-1681010.jpg'),
('486d0c5a-1d39-40e4-8480-3d012eb8f003','Carmelo Griffith','It is Not All It is Cracked Up To Be','media/man-wearing-santa-claus-costume-3149862.jpg'),
('530d576b-e7d6-4d9d-87a5-15d71fceb63c','Ted Calderon','Fight Fire With Fire','media/woman-s-in-red-sari-portrait-764529.jpg'),
('5a5e1e96-2580-48aa-8423-3536a8cc4941','Janis Hahn','Give a Man a Fish','media/man-in-black-shirt-35065.jpg'),
('63a00c07-cdc8-4de9-9870-16e0c1feed4c','Sydney Guerrero','Wake Up Call','media/woman-wearing-white-polo-long-sleeved-shirt-1181695.jpg'),
('8cd9265d-2078-40d1-bd9f-04a0aecb42fe','Shana Jennings','Jig Is Up','media/praying-man-looking-up-1438275.jpg'),
('906754aa-3788-4ba9-915f-272bbc4bc22f','Michel Taylor','Jumping the Gun','media/adult-beard-boy-casual-220453.jpg'),
('be6426d2-925c-4d55-9a02-438f3c4105ed','Jessica Yang','Tug of War','media/man-wearing-dress-shirt-holding-dandelion-1250426.jpg'),
('bf7c7966-a21c-4116-8d03-d42141412a55','Dollie Cochran','Under the Weather','media/woman-wearing-round-black-framed-eyeglasses-1845534.jpg'),
('c4e2bb9e-6a28-4682-adac-f36c6ca1660c','Wilton Robles','Scot-free','media/woman-in-yellow-tank-top-3761077.jpg'),
('caa20a29-f3e6-4f89-b781-6e6a25be68b4','Sung Carr','Would Not Harm a Fly','media/adult-black-pug-1851164.jpg'),
('d6bf2c11-4353-45d2-9bd5-8a5e0bf002be','Melba Smith','Up In Arms','media/man-wearing-black-framed-eyeglasses-2955247.jpg'),
('d9bac5df-8075-4758-915b-baf3f8326c2a','Monty Sexton','Swinging For the Fences','media/unknown-person-facing-sideways-2495720.jpg'),
('e0e98aa1-8ea8-490a-9492-d385b0d8fd3b','Bruce Cabrera','Elephant in the Room','media/close-up-photo-of-man-wearing-black-suit-jacket-756484.jpg'),
('ef8ad17d-b12c-495e-883e-d9b807585805','Antione Mackentire','Heads Up','media/close-up-photo-of-woman-with-horns-2020911.jpg'),
('f9326f30-e14c-4340-8f26-d3eb6618604e','Marsha Warner','Cup Of Joe','media/man-wearing-white-crew-neck-shirt-and-grey-denim-button-up-1993094.jpg')
GO

INSERT INTO users (Username,Password) 
VALUES 
('jprieto','siropederable')
GO