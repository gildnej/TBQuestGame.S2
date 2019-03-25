using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfTheAionProject.Models;
using WpfTheAionProject;
using System.Windows.Threading;
using System.Collections.ObjectModel;

namespace WpfTheAionProject.PresentationLayer
{
    /// <summary>
    /// view model for the game session view
    /// </summary>
    public class GameSessionViewModel : ObservableObject
    {
        #region ENUMS

        #endregion

        #region FIELDS

        private DateTime _gameStartTime;
        private string _gameTimeDisplay;
        private TimeSpan _gameTime;

        private Player _player;

        private Map _gameMap;
        private Location _currentLocation;
        private Location _forward, _right, _back, _left;

        #endregion

        #region PROPERTIES

        public Player Player
        {
            get { return _player; }
            set { _player = value; }
        }

        public string MessageDisplay
        {
            get { return _currentLocation.Message; }
        }
        public Map GameMap
        {
            get { return _gameMap; }
            set { _gameMap = value; }
        }
        public Location CurrentLocation
        {
            get { return _currentLocation; }
            set
            {
                _currentLocation = value;
                OnPropertyChanged(nameof(CurrentLocation));
            }
        }

        //
        // expose information about travel points from current location
        //
        public Location Forward
        {
            get { return _forward; }
            set
            {
                _forward = value;
                OnPropertyChanged(nameof(Forward));
                OnPropertyChanged(nameof(HasForwardLocation));
            }
        }

        public Location Right
        {
            get { return _right; }
            set
            {
                _right = value;
                OnPropertyChanged(nameof(Right));
                OnPropertyChanged(nameof(HasRightLocation));
            }
        }

        public Location Back
        {
            get { return _back; }
            set
            {
                _back = value;
                OnPropertyChanged(nameof(Back));
                OnPropertyChanged(nameof(HasBackLocation));
            }
        }

        public Location Left
        {
            get { return _left; }
            set
            {
                _left = value;
                OnPropertyChanged(nameof(Left));
                OnPropertyChanged(nameof(HasLeftLocation));
            }
        }

        public bool HasForwardLocation
        {
            get
            {
                if (Forward != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        //
        // shortened code with same functionality as above
        //
        public bool HasRightLocation { get { return Right != null; } }
        public bool HasBackLocation { get { return Back != null; } }
        public bool HasLeftLocation { get { return Left != null; } }

        public string TaskTimeDisplay
        {
            get { return _gameTimeDisplay; }
            set
            {
                _gameTimeDisplay = value;
                OnPropertyChanged(nameof(TaskTimeDisplay));
            }
        }

        #endregion

        #region CONSTRUCTORS

        public GameSessionViewModel()
        {

        }

        public GameSessionViewModel(
            Player player,
            Map gameMap,
            GameMapCoordinates currentLocationCoordinates)
        {
            _player = player;

            _gameMap = gameMap;
            _gameMap.CurrentLocationCoordinates = currentLocationCoordinates;
            _currentLocation = _gameMap.CurrentLocation;
            InitializeView();

            GameTimer();
        }

        #endregion

        #region METHODS

        /// <summary>
        /// game time event, publishes every 1 second
        /// </summary>
        public void GameTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += OnGameTimerTick;
            timer.Start();
        }

        /// <summary>
        /// calculate the available travel points from current location
        /// game slipstreams are a mapping against the 2D array where 
        /// </summary>
        private void UpdateAvailableTravelPoints()
        {
            //
            // reset travel location information
            //
            Forward = null;
            Right = null;
            Back = null;
            Left = null;

            if (_gameMap.FordwardLocation(_player) != null)
            {
                Forward = _gameMap.FordwardLocation(_player);
            }

            if (_gameMap.RightLocation(_player) != null)
            {
                Right = _gameMap.RightLocation(_player);
            }

            if (_gameMap.BackLocation(_player) != null)
            {
                Back = _gameMap.BackLocation(_player);
            }

            if (_gameMap.LeftLocation(_player) != null)
            {
                Left = _gameMap.LeftLocation(_player);
            }
        }

        ///// <summary>
        ///// player move event handler
        ///// </summary>
        private void OnPlayerMove()
        {
            //
            // update player stats when in new location
            //
            if (!_player.HasVisited(_currentLocation))
            {
                //
                // add location to list of visited locations
                //
                _player.LocationsVisited.Add(_currentLocation);

                // 
                // update experience points
                //
                _player.Sanity += _currentLocation.ModifySanity;



                //
                // update lives
                //
                if (_currentLocation.ModifyLives != 0) _player.Lives += _currentLocation.ModifyLives;

                //
                // display a new message if available
                //
                OnPropertyChanged(nameof(MessageDisplay));
            }
        }

        ///// <summary>
        ///// travel north
        ///// </summary>
        public void MoveNorth()
        {
            if (HasForwardLocation)
            {
                _gameMap.MoveForward();
                CurrentLocation = _gameMap.CurrentLocation;
                UpdateAvailableTravelPoints();
                OnPlayerMove();
            }
        }

        ///// <summary>
        ///// travel east
        ///// </summary>
        public void MoveEast()
        {
            if (HasRightLocation)
            {
                _gameMap.MoveRight();
                CurrentLocation = _gameMap.CurrentLocation;
                UpdateAvailableTravelPoints();
                OnPlayerMove();
            }
        }

        ///// <summary>
        ///// travel south
        ///// </summary>
        public void MoveSouth()
        {
            if (HasBackLocation)
            {
                _gameMap.MoveBack();
                CurrentLocation = _gameMap.CurrentLocation;
                UpdateAvailableTravelPoints();
                OnPlayerMove();
            }
        }

        ///// <summary>
        ///// travel west
        ///// </summary>
        public void MoveWest()
        {
            if (HasLeftLocation)
            {
                _gameMap.MoveLeft();
                CurrentLocation = _gameMap.CurrentLocation;
                UpdateAvailableTravelPoints();
                OnPlayerMove();
            }
        }

        #region GAME TIME METHODS

        /// <summary>
        /// running time of game
        /// </summary>
        /// <returns></returns>
        private TimeSpan GameTime()
        {
            return DateTime.Now - _gameStartTime;
        }

        /// <summary>
        /// game timer event handler
        /// 1) update mission time on window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnGameTimerTick(object sender, EventArgs e)
        {
            _gameTime = DateTime.Now - _gameStartTime;
            TaskTimeDisplay = "Task Time " + _gameTime.ToString(@"hh\:mm\:ss");
        }

        /// <summary>
        /// initial setup of the game session view
        /// </summary>
        private void InitializeView()
        {
            _gameStartTime = DateTime.Now;
            UpdateAvailableTravelPoints();
        }

        #endregion

        #endregion

        #region EVENTS



        #endregion
    }

}
