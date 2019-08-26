# Web API for DynamoDB Testing

## Run

```
docker-compose up -d --build
```

This command runs a local dynamodb container, runs a migration container to create initial table (`WidgetStates`) and start `api` container

## Queries

- Save widget state by organization id and user id

```
PUT http://localhost/api/widgetstates/organization/{organizationId}/user/{userId}/widget/{widgetName}
```

	Request Body

	```
	{
		"key-1": "value-1",
		"key-2": "value-2",
	}
	```

- Save widget state by organization id

```
PUT http://localhost/api/widgetstates/organization/{organizationId}/widget/{widgetName}
```

	Request Body

	```
	{
		"key-1": "value-1",
		"key-2": "value-2",
	}
	```

- Get widget states by organization id and user id

```
GET http://localhost/api/widgetstates/organization/{organizationId}/user/{userId}
```

- Get widget states by organization id

```
GET http://localhost/api/widgetstates/organization/{organizationId}
```

