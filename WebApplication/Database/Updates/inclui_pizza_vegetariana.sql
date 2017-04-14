START TRANSACTION;
USE `pizzariasognare.com.br`;


INSERT INTO `pizzariasognare.com.br`.`PizzaFlavor` (`id`, `name`, `image`, `enabled`) VALUES (40, 'Vegetariana', NULL, DEFAULT);

INSERT INTO `pizzariasognare.com.br`.`PizzaFlavorIngredient` (`pizza_flavor_id`, `ingredient_id`) VALUES (40, 1);
INSERT INTO `pizzariasognare.com.br`.`PizzaFlavorIngredient` (`pizza_flavor_id`, `ingredient_id`) VALUES (40, 3);
INSERT INTO `pizzariasognare.com.br`.`PizzaFlavorIngredient` (`pizza_flavor_id`, `ingredient_id`) VALUES (40, 13);
INSERT INTO `pizzariasognare.com.br`.`PizzaFlavorIngredient` (`pizza_flavor_id`, `ingredient_id`) VALUES (40, 8);
INSERT INTO `pizzariasognare.com.br`.`PizzaFlavorIngredient` (`pizza_flavor_id`, `ingredient_id`) VALUES (40, 17);

INSERT INTO `pizzariasognare.com.br`.`Pizza` (`id`, `pizza_flavor_id`, `pizza_size_id`, `price`, `enabled`) VALUES (118, 40, 1, 25.9, DEFAULT);
INSERT INTO `pizzariasognare.com.br`.`Pizza` (`id`, `pizza_flavor_id`, `pizza_size_id`, `price`, `enabled`) VALUES (119, 40, 2, 36.9, DEFAULT);
INSERT INTO `pizzariasognare.com.br`.`Pizza` (`id`, `pizza_flavor_id`, `pizza_size_id`, `price`, `enabled`) VALUES (120, 40, 3, 47.9, DEFAULT);

COMMIT;