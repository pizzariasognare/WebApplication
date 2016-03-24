START TRANSACTION;
USE `pizzariasognare.com.br`;
INSERT INTO `pizzariasognare.com.br`.`Drink` (`id`, `drink_type_id`, `name`, `price`, `image`, `enabled`) VALUES (1, 1, 'Coca-Cola 2L', 10.00, NULL, DEFAULT);
INSERT INTO `pizzariasognare.com.br`.`Drink` (`id`, `drink_type_id`, `name`, `price`, `image`, `enabled`) VALUES (2, 1, 'Guaraná Antártica 2L', 08.00, NULL, DEFAULT);
INSERT INTO `pizzariasognare.com.br`.`Drink` (`id`, `drink_type_id`, `name`, `price`, `image`, `enabled`) VALUES (3, 1, 'Fanta Laranja 2L', 08.00, NULL, DEFAULT);
INSERT INTO `pizzariasognare.com.br`.`Drink` (`id`, `drink_type_id`, `name`, `price`, `image`, `enabled`) VALUES (4, 1, 'Fanta Uva 2L', 08.00, NULL, DEFAULT);
INSERT INTO `pizzariasognare.com.br`.`Drink` (`id`, `drink_type_id`, `name`, `price`, `image`, `enabled`) VALUES (5, 1, 'Sprite 2L', 08.00, NULL, DEFAULT);
INSERT INTO `pizzariasognare.com.br`.`Drink` (`id`, `drink_type_id`, `name`, `price`, `image`, `enabled`) VALUES (6, 1, 'Coca-Cola Zero 2L', 10.00, NULL, DEFAULT);
COMMIT;