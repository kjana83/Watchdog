## Watchdog

Watch services and reports thes status around the clock

## Sentry

Sentry is the Windows service. It will monitor the services and logs in the Redis.

## Canary

Cenary is web site, you can add/update the service details through the admin page, it adds in the Redis db.
Canary has Service page can be used for the current status on the service.

Redis DB name can be modified in App.Config and Web config respectively in Windows and Web applications.
Make sure that these names are in Sync.

## Installation Guidelines

1. Install Redis as a service with Persistent connnetion. Start the service.
2. Unzip Deploy/Watchdog.zip
3. Install Sentry service. InstallUtil .../Sentry.exe -i
4. Create IIS Website pointing to Canary folder
5. Access the site now. http://x.x.x.x:xxxx/Site/Admin 
6. Test and Add the services 
7. Start the Sentry service
8. Go to site http://x.x.x.x:xxxx/Site/Admin Monitoring result will populate now.
