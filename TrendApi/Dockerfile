FROM openjdk:11

WORKDIR /app

COPY src ./src
COPY gradle ./gradle
COPY build.gradle ./build.gradle
COPY gradlew ./gradlew
COPY settings.gradle ./settings.gradle

RUN ./gradlew build

EXPOSE 7779
ENTRYPOINT ["sh", "gradlew", "run"]