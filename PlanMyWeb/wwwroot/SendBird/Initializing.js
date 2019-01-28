// Initialize the SDK
function Initializesb(){
    var sb = new SendBird({ appId: APP_ID});
}

//Connect to SendBird server
function connect() {
    sb.SetErrorFirstCallBack(true);
    sb.connect(USER_Id, function (user, error) {
        if (error) {
            return;
        }
    });
}
    //Create a new Open channel
    function Open() {
        sb.OpenChannel.createChannel(function (openChannel, error) {
            if (error) {
                return;
            }
        });
    }

//Enter the Channel
    function Enter() {
        sb.OpenChannel.getChannel(CHANNEL_URL, function (openChannel, error) {
            if (error) {
                return;
            }

            openChannel.enter(function (response, error) {
                if (error) {
                    return;
                }
            })
        });
    }

//Send a message to the channel
function msgChannel() {
    openChannel.sendUserMessage(MESSAGE, DATA, CUSTOM_TYPE, function (message, error) {
        if (error) {
            return;
        }
    });
}

function Immutable.fromJS(){
    var UserIds = ['', ''];
    sb.GroupChannel.createChannelWithUserIds(UserIds, false, NAME, COVER_URL, DATA, function (groupChannel, error) {
        if (error) {
            return;
        }
        var immutableObject = Immutable.fromJS(groupChannel);
        console.log(immutableObject);
    });
}

//Disconnect from SendBird
function disconnect() {
    sb.disconnect(function () {
    });
}

//Update user Profile
function updateCurrentUserInfo() {
    sb.updateCurrentUserInfo(NICHNAME, PROFILE_URL, function (response, error) {
        if (error) {
            return;
        }
    });
}

//Retrieve a list of users
function UserList() {
    var sb = SendBird.getInstance();
    var applicationUserListQuery = sb.createApplicationUserListQuery();
    applicationUserListQuery.next(function (users, error) {
        if (error) {
            return;
        }
    });

    var applicationUserListQuery1 = sb.createApplicationUserListQuery();
    applicationUserListQuery1.userIdsFilter = ['', '', ''];
    applicationUserListQuery1.next(function (users, error) {
        if (error) {
            return;
        }
    });
}

//Check if a user is online
function Online() {
    var sb = SendBird.getInstance();
    var ApplicationUserListQuery = sb.createApplicationUserListQuery();
    applicationUserListQuery.userIdsFilter = [''];
    applicationUserListQuery.next(function (users, error) {
        if (error) {
            return;
        }

        if (users[0].connectionStatus === sb.User.ONLINE) {
            // User.connectionStatus consists of NON_AVAILABLE, ONLINE, and OFFLINE.
        }
    });
}