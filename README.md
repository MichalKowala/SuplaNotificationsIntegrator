# SuplaNotificationsIntegrator

An Azure function project. After each X minutes (by default 15) a timer trigger gets activated. The function then proceeeds to check measurements of each Supla device (www.supla.org) stored in a list of devices. Each measured property of each device has its maximum and minimum value thresholds. If these thresholds get exceeded the program will then send a notification (via email or SMS) to those who subsrcibed to watch given device. Besides that, after each measurment the program generates a report which then is stored in a Comos Database and logged in logs folder.
