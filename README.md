# TestingApi
An experimental asp.net core web api for learning new technologies.


### Endpoints

**GET /Channels/**
Lists channels active in the last 24 hours.

**GET /Channels/{name}/Messages/**
Gets messages for channel based on channel name.

**GET /Channels/{name}/Messages/After/{messageId}**
Gets messages for channel based on channel name after the message with the specified id.

**POST /Channels/{name}/Messages/**
Posts a new message to channel based on channel name.