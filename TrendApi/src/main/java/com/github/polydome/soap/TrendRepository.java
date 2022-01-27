package com.github.polydome.soap;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;
import java.util.Optional;

public class TrendRepository {
    private final Connection connection;

    public TrendRepository() throws SQLException {
        connection = DriverManager
                .getConnection("jdbc:mysql://localhost:3306/tsunami", "root", "tsunami");

    }

    Optional<Trend> findTrendByCategoryId(String categoryId) {
        try (final var getTrendByCategoryId = connection.prepareStatement("select articles_count, videos_count from categories_trend inner join categories c on c.id = categories_trend.category_id where c.id = ?")) {
            getTrendByCategoryId.setString(1, categoryId);

            try (final var rs = getTrendByCategoryId.executeQuery()) {
                if (!rs.next()) {
                    return Optional.empty();
                }
                return Optional.of(new Trend(
                        rs.getInt("articles_count"),
                        rs.getInt("videos_count")
                ));
            }
        } catch (SQLException e) {
            e.printStackTrace();
            return Optional.empty();
        }
    }
}
