package com.github.polydome.soap;

import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlRootElement;

@XmlRootElement(name = "greeting")
public class Trend {
    private int articlesCount;
    private int videosCount;

    public Trend(int articlesCount, int videosCount) {
        this.articlesCount = articlesCount;
        this.videosCount = videosCount;
    }

    public Trend() {
    }

    @XmlElement
    public int getArticlesCount() {
        return articlesCount;
    }

    public void setArticlesCount(int articlesCount) {
        this.articlesCount = articlesCount;
    }

    @XmlElement
    public int getVideosCount() {
        return videosCount;
    }

    public void setVideosCount(int videosCount) {
        this.videosCount = videosCount;
    }
}
