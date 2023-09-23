﻿using Domain.Interface.FirebaseMessagingService;
using Domain.Messages;
using FirebaseAdmin.Messaging;

namespace FCM.Messaging;

public class FirebaseMessagingService<T> : IFirebaseMessagingService<T> where T : IMessageModel
{
    public async Task SendMessageAsync<T>(T message) where T : IMessageModel
    {
        Message Message = ConvertToFirebaseMessage(message);

        await FirebaseMessaging.DefaultInstance.SendAsync(Message);
    }

    private Message ConvertToFirebaseMessage<T>(T messageModel) where T : IMessageModel
    {
        var firebaseMessage = new Message()
        {
            Notification = new Notification()
            {
                Title = messageModel.Title,
                Body = messageModel.Body
            },
            Token = messageModel.Token,
            Topic = messageModel.Topic
        };

        return firebaseMessage;
    }
}