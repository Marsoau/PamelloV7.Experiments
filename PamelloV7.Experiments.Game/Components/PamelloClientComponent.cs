using System;
using JetBrains.Annotations;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using PamelloV7.Wrapper;
using PamelloV7.Wrapper.Entities;

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
        if (user?.SelectedPlayerId == null) {
            SelectedPlayer.Value = null;
            return;
        }

        if (user.SelectedPlayerId == SelectedPlayer.Value?.Id) return;

        Client.PEQL.GetSingleAsync<RemotePlayer>(user.SelectedPlayerId.Value).ContinueWith(t => {
            SelectedPlayer.Value = t.Result;
        });
    }

    private void selectedPlayerChanged([CanBeNull] RemotePlayer player)
    {
        if (player?.Queue.CurrentSongId == null) {
            CurrentSong.Value = null;
            return;
        }

        if (player.Queue.CurrentSongId == CurrentSong.Value?.Id) return;

        Client.PEQL.GetSingleAsync<RemoteSong>(player.Queue.CurrentSongId.Value).ContinueWith(t => {
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
