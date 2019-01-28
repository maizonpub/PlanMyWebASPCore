import { console } from "./wwwroot/cms/bower_components/inputmask/dist/inputmask/global/window";

//Retrieve the last message of a channel
function getLastMessage() {
    groupChannel.lastMessage;
}

//Create a channel
function createChannel() {
    var userIds = ['', ''];
    // When 'distinct' is false
    sb.GroupChannel.createChannelWithUserIds(userIds, false, NAME, COVER_URL, DATA, function (groupChannel, error) {
        if (error) {
            return;
        }
        console.log(groupChannel);
    });
}

//Invite users as members
function inviteUser() {
    var userIds = ['', ''];
    groupChannel.inviteWithUserIds(userIds, function (response, error) {
        if (error) {
            return;
        }
    });
}

//Retrieve a list of all members
function listMembers() {
    groupChannel.members;
}

//Filter channels by userIDs
function filterChannel() {
    var filteredQuery = GroupChannel.createMyGroupChanelListQuery();
    filteredQuery.includeEmty = true;
    filteredQuery.hiddenChannelFilter = sb.GroupChannel.hiddenChannelFilter.HIDDEN_PREVENT_AUTO_UNHIDE;
    filteredQuery.next(function (channels, error) {
        if (error) {
            return;
        }
    });
}

//Send a message
function sendMessage() {
    channel.sendUserMessage(MESSAGE, DATA, CUSTOM_TYPE, function (message, error) {
        if (error) {
            return;
        }
        console.log(message);
    })
}

//Retrieve a list of channels
function ChannelsList() {
    var openChannelListQuery = sb.OpenChannel.createOpenChannelListQuery();

    openChannelListQuery.next(function (channels, error) {
        if (error) {
            return;
        }

        console.log(channels);
    });
}

//Register APNs device token and FCM registration token of users to SendBird server
  const sb = SendBird.getInstance();

    if (sb) {
        if (Platform.OS === 'ios') {
            sb.registerAPNSPushTokenForCurrentUser(TOKEN, function (response, error) {
                if (error) {
                    return;
                }

                // Do something in response to a successful registeration.
            });
        } else {
            sb.registerGCMPushTokenForCurrentUser(TOKEN, function (response, error) {
                if (error) {
                    return;
                }

                // Do something in response to a successful registeration.
            });
        }
    }


//Send and receive a push notification
function PushNoti{
    import FCM, {
        FCMEvent
    } from "react-native-fcm";

    const showLocalNotificationWithAction = notif => {
        try {
            const data = JSON.parse(notif.sendbird);
            FCM.presentLocalNotification({
                title: data.sender ? data.sender.name : 'SendBird',
                body: data.message,
                priority: "high",
                show_in_foreground: true,
                click_action: "com.sendbird.sample.reactnative" // for ios
            });
        } catch (e) {
        }
    }

    FCM.on(FCMEvent.Notification, notif => {
        showLocalNotificationWithAction(notif);
    });
}












