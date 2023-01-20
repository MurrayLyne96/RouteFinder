import { css } from "@emotion/react";
import { ThemeOptions } from "@mui/material";

export const headerCss = css({
    color: "#45818e",
    fontFamily: "Balsamiq Sans, Sans Serif",
    marginBottom: "2%"
});

export const font52 = css ({
    fontSize: 52
});

export const font36 = css ({
  fontSize: 36
});

export const formGroup = css ({
    display: 'flex',
    flexDirection: 'column',
    width: '20%',
    marginBottom: '1%',
});

export const alignItemsLeft = css ({
    display: 'flex',
    alignItems: 'start'
});

export const justifyItemsLeft = css ({
    display: 'flex',
    justifyContent: 'left'
});

export const justifyItemsRight = css ({
    display: 'flex',
    justifyContent: 'end'
});

export const loginButtonsBottom = css ({
    display: 'flex',
    flexDirection: 'row',
});

export const flexBasis50 = css ({
    flexBasis: '50%'
});

export const centeredItem = css ({
    display: 'flex',
    alignItems: 'center'
});

export const halfWidth = css ({
    width: '50%'
});

export const fullWidth = css ({
  width: '100%'
});

export const thirdWidth = css ({
  width: '33.3%'
});

export const finderThemeOptions: ThemeOptions = {
    palette: {
      primary: {
        main: '#2b3f9a',
        light: '#597dfb',
        dark: '#718af3',
        contrastText: '#ffffff',
      },
      secondary: {
        main: '#3b3a3a',
      },
      success: {
        main: '#014812',
      },
      divider: '#088787',
      error: {
        main: '#d50000',
      },
    },
    typography: {
      fontSize: 16,
      fontFamily: 'Lato',
      fontWeightLight: 300,
      fontWeightBold: 1000,
      fontWeightMedium: 700,
      fontWeightRegular: 500,
    },
    components: {
        MuiTypography: {
            defaultProps: {
                fontFamily: 'Lato'
            }
        }
    },
    
  };