DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `Sp_Restauranes`(
	IN _option char(1),
    IN _nombre_restaurante varchar(50),
    IN _id_restaurante int,
    IN _id_ciudad int,
    IN _numero_aforo int,
    IN _telefono varchar(15)
)
BEGIN
	CASE _option
		WHEN 'S' THEN

			SELECT IDRestaurante,NombreRestaurante,Restaurantes.IDCiudad,NombreCiudad,NumeroAforo,Telefono,Restaurantes.FechaCreacion FROM Restaurantes INNER JOIN Ciudad ON Restaurantes.IDCiudad = Ciudad.IDCiudad AND IDRestaurante = _id_restaurante;
		WHEN 'I' THEN

			INSERT INTO Restaurantes (NombreRestaurante,IDCiudad,NumeroAforo,Telefono) VALUES(_nombre_restaurante,_id_ciudad,_numero_aforo,_telefono);

		WHEN 'U' THEN

			UPDATE Restaurantes SET NombreRestaurante = _nombre_restaurante,IDCiudad = _id_ciudad,NumeroAforo = _numero_aforo,Telefono = _telefono  WHERE IDRestaurante = _id_restaurante;

		WHEN 'D' THEN

			DELETE FROM Restaurantes WHERE IDRestaurante = _id_restaurante;

		ELSE 
			SELECT IDRestaurante,NombreRestaurante,Restaurantes.IDCiudad,NombreCiudad,NumeroAforo,Telefono,Restaurantes.FechaCreacion FROM Restaurantes INNER JOIN Ciudad ON Restaurantes.IDCiudad = Ciudad.IDCiudad;
	END CASE;
END$$
DELIMITER ;
