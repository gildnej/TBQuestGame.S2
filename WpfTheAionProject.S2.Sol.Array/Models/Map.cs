using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTheAionProject.Models
{
    /// <summary>
    /// game map class
    /// </summary>
    public class Map
    {
        #region FIELDS

        private Location[,] _mapLocations;
        private int _maxRows, _maxColumns;
        private GameMapCoordinates _currentLocationCoordinates;

        #endregion

        #region PROPERTIES

        public Location[,] MapLocations
        {
            get { return _mapLocations; }
            set { _mapLocations = value; }
        }

        public GameMapCoordinates CurrentLocationCoordinates
        {
            get { return _currentLocationCoordinates; }
            set { _currentLocationCoordinates = value; }
        }

        public Location CurrentLocation
        {
            get { return _mapLocations[_currentLocationCoordinates.Row, _currentLocationCoordinates.Column]; }
        }

        #endregion


        #region CONSTRUCTORS

        public Map(int rows, int columns)
        {
            _maxRows = rows;
            _maxColumns = columns;
            _mapLocations = new Location[rows, columns];
        }

        #endregion


        #region METHODS

        public void MoveForward()
        {
            //
            // not on forward border
            //
            if (_currentLocationCoordinates.Row > 0)
            {
                _currentLocationCoordinates.Row -= 1;
            }
        }

        public void MoveRight()
        {
            //
            // not on right border
            //
            if (_currentLocationCoordinates.Column < _maxColumns - 1)
            {
                _currentLocationCoordinates.Column += 1;
            }
        }

        public void MoveBack()
        {
            if (_currentLocationCoordinates.Row < _maxRows - 1)
            {
                _currentLocationCoordinates.Row += 1;
            }
        }

        public void MoveLeft()
        {
            //
            // not on left border
            //
            if (_currentLocationCoordinates.Column > 0)
            {
                _currentLocationCoordinates.Column -= 1;
            }
        }

        //
        // get the forward location if it exists
        //
        public Location FordwardLocation(Player player)
        {
            Location forwardLocation = null;

            //
            // not on forward border
            //
            if (_currentLocationCoordinates.Row > 0)
            {
                Location nextForwardLocation = _mapLocations[_currentLocationCoordinates.Row - 1, _currentLocationCoordinates.Column];

                //
                // location exists and player can access location
                //
                if (nextForwardLocation != null && nextForwardLocation.Accessible == true)
                {
                    forwardLocation = nextForwardLocation;
                }
            }

            return forwardLocation;
        }

        //
        // get the east location if it exists
        //
        public Location RightLocation(Player player)
        {
            Location rightLocation = null;

            //
            // not on east border
            //
            if (_currentLocationCoordinates.Column < _maxColumns - 1)
            {
                Location nextRightLocation = _mapLocations[_currentLocationCoordinates.Row, _currentLocationCoordinates.Column + 1];

                //
                // location exists and player can access location
                //
                if (nextRightLocation != null && nextRightLocation.Accessible == true)
                {
                    rightLocation = nextRightLocation;
                }
            }

            return rightLocation;
        }

        //
        // get the back location if it exists
        //
        public Location BackLocation(Player player)
        {
            Location backLocation = null;

            //
            // not on back border
            //
            if (_currentLocationCoordinates.Row < _maxRows - 1)
            {
                Location nextBackLocation = _mapLocations[_currentLocationCoordinates.Row + 1, _currentLocationCoordinates.Column];

                //
                // location exists and player can access location
                //
                if (nextBackLocation != null && nextBackLocation.Accessible == true)
                {
                    backLocation = nextBackLocation;
                }
            }

            return backLocation;
        }

        //
        // get the Left location if it exists
        //
        public Location LeftLocation(Player player)
        {
            Location leftLocation = null;

            //
            // not on Left border
            //
            if (_currentLocationCoordinates.Column > 0)
            {
                Location nextLeftLocation = _mapLocations[_currentLocationCoordinates.Row, _currentLocationCoordinates.Column - 1];

                //
                // location exists and player can access location
                //
                if (nextLeftLocation != null &&(nextLeftLocation.Accessible == true))
                {
                    leftLocation = nextLeftLocation;
                }
            }

            return leftLocation;
        }

        #endregion
    }
}
