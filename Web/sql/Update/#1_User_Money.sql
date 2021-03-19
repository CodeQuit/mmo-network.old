ALTER TABLE `users` DROP `company`;
ALTER TABLE `users` ADD `money` DOUBLE NOT NULL DEFAULT '0' AFTER `last_name`;