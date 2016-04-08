START TRANSACTION;
USE `pizzariasognare.com.br`;

INSERT INTO `pizzariasognare.com.br`.`Ingredient` (`id`, `name`) VALUES (35, 'Salame');

INSERT INTO `pizzariasognare.com.br`.`PizzaFlavor` (`id`, `name`, `image`, `enabled`) VALUES (38, 'salame', NULL, DEFAULT);

INSERT INTO `pizzariasognare.com.br`.`PizzaFlavorIngredient` (`pizza_flavor_id`, `ingredient_id`) VALUES (38, 1);
INSERT INTO `pizzariasognare.com.br`.`PizzaFlavorIngredient` (`pizza_flavor_id`, `ingredient_id`) VALUES (38, 35);
INSERT INTO `pizzariasognare.com.br`.`PizzaFlavorIngredient` (`pizza_flavor_id`, `ingredient_id`) VALUES (38, 3);

INSERT INTO `pizzariasognare.com.br`.`Pizza` (`id`, `pizza_flavor_id`, `pizza_size_id`, `price`, `enabled`) VALUES (112, 38, 1, 20.9, DEFAULT);
INSERT INTO `pizzariasognare.com.br`.`Pizza` (`id`, `pizza_flavor_id`, `pizza_size_id`, `price`, `enabled`) VALUES (113, 38, 2, 30.9, DEFAULT);
INSERT INTO `pizzariasognare.com.br`.`Pizza` (`id`, `pizza_flavor_id`, `pizza_size_id`, `price`, `enabled`) VALUES (114, 38, 3, 40.9, DEFAULT);

COMMIT;