#!/bin/sh

TABLE_STATUS=$(aws dynamodb list-tables --endpoint-url http://dynamodb:8000 --region ap-southeast-2 --output json | jq -r '.TableNames[]' | grep WidgetStates)
if [[ -z "${TABLE_STATUS}" ]]; then
    aws dynamodb create-table \
        --table-name WidgetStates \
        --attribute-definitions \
            AttributeName=UserId,AttributeType=N \
            AttributeName=OrganizationId,AttributeType=N \
        --key-schema \
            AttributeName=UserId,KeyType=HASH \
            AttributeName=OrganizationId,KeyType=RANGE \
        --provisioned-throughput \
            ReadCapacityUnits=10,WriteCapacityUnits=5 \
        --endpoint-url \
            http://dynamodb:8000 \
        --region \
            ap-southeast-2
fi

aws dynamodb put-item \
    --table-name WidgetStates  \
    --item \
        '{"UserId": {"N": "1"}, "OrganizationId": {"N": "1"}, "WidgetName": {"S": "widget-1"}, "States": {"M": {"state-1": { "S": "value-1"}, "state-2": { "S": "value-2"}}}}' \
    --endpoint-url \
        http://dynamodb:8000 \
    --region \
        ap-southeast-2

aws dynamodb put-item \
    --table-name WidgetStates  \
    --item \
        '{"UserId": {"N": "2"}, "OrganizationId": {"N": "2"}, "WidgetName": {"S": "widget-2"}, "States": {"M": {"state-1": { "S": "value-1"}, "state-2": { "S": "value-2"}}}}' \
    --endpoint-url \
        http://dynamodb:8000 \
    --region \
        ap-southeast-2
