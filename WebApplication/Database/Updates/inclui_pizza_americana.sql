START TRANSACTION;
USE `pizzariasognare.com.br`;


INSERT INTO `pizzariasognare.com.br`.`PizzaFlavor` (`id`, `name`, `image`, `enabled`) VALUES (39, 'Americana', NULL, DEFAULT);

INSERT INTO `pizzariasognare.com.br`.`PizzaFlavorIngredient` (`pizza_flavor_id`, `ingredient_id`) VALUES (39, 1);
INSERT INTO `pizzariasognare.com.br`.`PizzaFlavorIngredient` (`pizza_flavor_id`, `ingredient_id`) VALUES (39, 5);
INSERT INTO `pizzariasognare.com.br`.`PizzaFlavorIngredient` (`pizza_flavor_id`, `ingredient_id`) VALUES (39, 4);

INSERT INTO `pizzariasognare.com.br`.`Pizza` (`id`, `pizza_flavor_id`, `pizza_size_id`, `price`, `enabled`) VALUES (115, 39, 1, 25.9, DEFAULT);
INSERT INTO `pizzariasognare.com.br`.`Pizza` (`id`, `pizza_flavor_id`, `pizza_size_id`, `price`, `enabled`) VALUES (116, 39, 2, 36.9, DEFAULT);
INSERT INTO `pizzariasognare.com.br`.`Pizza` (`id`, `pizza_flavor_id`, `pizza_size_id`, `price`, `enabled`) VALUES (117, 39, 3, 47.9, DEFAULT);

COMMIT;