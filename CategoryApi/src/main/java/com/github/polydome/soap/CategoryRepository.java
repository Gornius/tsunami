package com.github.polydome.soap;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;
import java.util.Optional;

public class CategoryRepository {
    private final Connection connection;

    public CategoryRepository() throws SQLException {
        connection = DriverManager
                .getConnection("jdbc:mysql://tsunami-db:3306/tsunami", "root", "tsunami");
    }

    Optional<String> findCategoryNameById(String categoryId) {
        try (final var getCategoryById = connection.prepareStatement("select id, title from categories c where c.id = ?")) {
            getCategoryById.setString(1, categoryId);

            try (final var rs = getCategoryById.executeQuery()) {
                if (!rs.next()) {
                    return Optional.empty();
                }
                return Optional.of(rs.getString("title"));
            }
        } catch (SQLException e) {
            e.printStackTrace();
            return Optional.empty();
        }
    }
}
