package com.github.polydome.soap;

import javax.jws.WebService;
import java.sql.SQLException;

@WebService(endpointInterface = "com.github.polydome.soap.CategoryInfoService")
public class LocalCategoryInfoService implements CategoryInfoService {
    private final CategoryRepository categoryRepository;

    public LocalCategoryInfoService(CategoryRepository categoryRepository) {
        this.categoryRepository = categoryRepository;
    }

    public LocalCategoryInfoService() throws SQLException {
        categoryRepository = new CategoryRepository();
    }

    @Override
    public String getCategoryById(String id) {
        return categoryRepository.findCategoryNameById(id).orElse("No such category");
    }
}
