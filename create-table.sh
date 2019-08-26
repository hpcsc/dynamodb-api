#!/bin/sh

TABLE_STATUS=$(aws dynamodb list-tables --endpoint-url http://dynamodb:8000 --region ap-southeast-2 --output json | jq -r '.TableNames[]' | grep WidgetStates)
if [[ -z "${TABLE_STATUS}" ]]; then
    aws dynamodb create-table \
        --table-name WidgetStates \
        --attribute-definitions \
            AttributeName=OrganizationId,AttributeType=N \
            AttributeName=SortKey,AttributeType=S \
        --key-schema \
            AttributeName=OrganizationId,KeyType=HASH \
            AttributeName=SortKey,KeyType=RANGE \
        --provisioned-throughput \
            ReadCapacityUnits=10,WriteCapacityUnits=5 \
        --endpoint-url \
            http://dynamodb:8000 \
        --region \
            ap-southeast-2
fi
