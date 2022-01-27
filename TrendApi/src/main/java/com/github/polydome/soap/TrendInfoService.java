package com.github.polydome.soap;

import javax.jws.WebMethod;
import javax.jws.WebService;
import javax.jws.soap.SOAPBinding;

@WebService
@SOAPBinding(style = SOAPBinding.Style.RPC)
public interface TrendInfoService {
    @WebMethod
    Trend getTrendByCategoryId(String id);
}
