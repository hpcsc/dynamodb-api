FROM jwilder/dockerize:0.6.1

WORKDIR /app

ADD ./create-table.sh .

ENV PATH="~/.local/bin:${PATH}"
RUN apk -Uuv add groff less python py-pip curl && \
    pip install awscli && \
    curl -L https://github.com/stedolan/jq/releases/download/jq-1.6/jq-linux64 -o /usr/local/bin/jq && \
    chmod +x /usr/local/bin/jq && \
    apk --purge -v del py-pip && \
    rm /var/cache/apk/*
    
ENTRYPOINT dockerize -wait tcp://dynamodb:8000 && ./create-table.sh
