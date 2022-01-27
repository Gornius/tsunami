package com.github.polydome.soap;

import javax.jws.WebService;
import java.sql.SQLException;

@WebService(endpointInterface = "com.github.polydome.soap.TrendInfoService")
public class LocalTrendInfoService implements TrendInfoService {
    private final TrendRepository trendRepository;

    public LocalTrendInfoService(TrendRepository trendRepository) {
        this.trendRepository = trendRepository;
    }

    public LocalTrendInfoService() throws SQLException {
        trendRepository = new TrendRepository();
    }

    @Override
    public Trend getTrendByCategoryId(String id) {
        return trendRepository.findTrendByCategoryId(id).orElse(new Trend());
    }
}
