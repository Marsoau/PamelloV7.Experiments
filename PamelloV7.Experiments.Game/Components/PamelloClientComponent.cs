using System;
using JetBrains.Annotations;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using PamelloV7.Wrapper;
using PamelloV7.Wrapper.Entities;
using PamelloV7.Wrapper.Extensions;

namespace PamelloV7.Experiments.Game.Components;

public partial class PamelloClientComponent : Component
{
    public PamelloClient Client { get; } = new();

    public Bindable<EConnectionState> ConnectionState { get; } = new();

    public BindableBool IsConnected { get; } = new();
    public BindableBool IsAuthorized { get; } = new();

    public Bindable<RemoteUser> User { get; } = new();
    public Bindable<RemotePlayer> SelectedPlayer { get; } = new();
    public Bindable<RemoteSong> CurrentSong { get; } = new();

    private void isAuthorizedChanged(ValueChangedEvent<bool> e) {
        User.Value = Client.User;
    }

    private void userChanged([CanBeNull] RemoteUser user) {
        if (user is null || user.SelectedPlayer.Id == 0) {
            SelectedPlayer.Value = null;
            return;
        }

        if (user.SelectedPlayer.Id == (SelectedPlayer.Value?.Id ?? 0)) return;

        user.SelectedPlayer.LoadAsync().ContinueWith(t => {
            SelectedPlayer.Value = t.Result;
        });
    }

    private void selectedPlayerChanged([CanBeNull] RemotePlayer player)
    {
        if (player is null || player.Queue.CurrentSong.Id == 0) {
            CurrentSong.Value = null;
            return;
        }

        if (player.Queue.CurrentSong.Id == (CurrentSong.Value?.Id ?? 0)) return;

        player.Queue.CurrentSong.LoadAsync().ContinueWith(t => {
            CurrentSong.Value = t.Result;
        });
    }

    protected override void LoadComplete() {
        base.LoadComplete();

        IsAuthorized.BindValueChanged(isAuthorizedChanged);

        User.BindValueChanged(v => Scheduler.Add(() => userChanged(v.NewValue)));
        SelectedPlayer.BindValueChanged(v => Scheduler.Add(() => selectedPlayerChanged(v.NewValue)));

        Client.OnConnectionStateChanged += () => ConnectionState.Value = Client.ConnectionState;

        Client.OnConnected += _ => IsConnected.Value = true;
        Client.OnDisconnected += _ => IsConnected.Value = false;

        Client.OnAuthorized += _ => IsAuthorized.Value = true;
        Client.OnUnauthorized += _ => IsAuthorized.Value = false;

        Client.Events.Watch(() => {
            userChanged(User.Value);
        }, () => [User.Value]);

        Client.Events.Watch(() => {
            selectedPlayerChanged(SelectedPlayer.Value);
        }, () => [SelectedPlayer.Value]);
    }

    protected override void Dispose(bool isDisposing) {
        base.Dispose(isDisposing);

        Client.DisconnectAsync();
    }
}
