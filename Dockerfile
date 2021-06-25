FROM ubuntu:18.04

ARG EXECUTABLE
ENV EXECUTABLE ${EXECUTABLE}

EXPOSE 7777/udp
EXPOSE 7777/tcp

COPY ./Server /server

WORKDIR /server

CMD ./${EXECUTABLE}