CREATE TABLE `categories`
(
    `id`    varchar(11) PRIMARY KEY,
    `title` varchar(255)
);

CREATE TABLE `tags`
(
    `title` varchar(255) PRIMARY KEY
);

CREATE TABLE `categories_trend`
(
    `id`             int PRIMARY KEY AUTO_INCREMENT,
    `category_id`    varchar(11),
    `articles_count` int,
    `videos_count`   int,
    `trend_time`     time
);

CREATE TABLE `tags_trend`
(
    `id`             int PRIMARY KEY AUTO_INCREMENT,
    `tag_title`      varchar(255),
    `articles_count` int,
    `videos_count`   int,
    `trend_time`     time
);

ALTER TABLE `categories_trend`
    ADD FOREIGN KEY (`category_id`) REFERENCES `categories` (`id`);

ALTER TABLE `tags_trend`
    ADD FOREIGN KEY (`tag_title`) REFERENCES `tags` (`title`);
