package com.github.polydome.soap;

import javax.xml.ws.Endpoint;
import java.sql.SQLException;

class Publisher {
    public static void main(String[] args) throws SQLException {
        var repository = new TrendRepository();
        Endpoint.publish("http://localhost:7779/ws/first", new LocalTrendInfoService(repository));
    }
}
