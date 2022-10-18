using Auxiliary.Configuration;
using Microsoft.Xna.Framework;
using System.Timers;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;
using TShockAPI.Hooks;
using Timer = System.Timers.Timer;

namespace Herald
{
    [ApiVersion(2, 1)]
    public class Herald : TerrariaPlugin
    {
        private readonly List<Timer> _timers;

        public override string Author
            => "TBC Developers";

        public override string Description
            => "A plugin that automatically announces messages at certain intervals.";

        public override string Name
            => "Herald";

        public override Version Version
            => new(1, 0);

        public Herald(Main game)
            : base(game)
        {
            _timers = new();
            Order = 1;
        }

        public override void Initialize()
        {
            Configuration<HeraldSettings>.Load("Herald");

            GeneralHooks.ReloadEvent += (x) =>
            {
                Configuration<HeraldSettings>.Load("Herald");
                ResetTimers();

                x.Player.SendSuccessMessage("[Herald] Reloaded configuration.");
            };

            ResetTimers();
        }

        private void ResetTimers()
        {
            foreach (var timer in _timers)
            {
                timer.Stop();
                timer.Dispose();
            }
            _timers.Clear();

            foreach (var announcement in Configuration<HeraldSettings>.Settings.Announcements)
                _timers.Add(Configure(announcement));
        }

        private Timer Configure(Announcement announcement)
        {
            var timer = new Timer(announcement.Interval * 1000)
            {
                AutoReset = true,
                Enabled = true,
            };

            timer.Elapsed += (_, x)
                => Tick(x, announcement);

            timer.Start();

            return timer;
        }

        private void Tick(ElapsedEventArgs _, Announcement announcement)
        {
            foreach (var command in announcement.Actions)
                Commands.HandleCommand(TSPlayer.Server, command);

            foreach (var line in announcement.Message)
                TShock.Utils.Broadcast(line, Color.Yellow);
        }
    }
}