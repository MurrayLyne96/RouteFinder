import { css } from "@emotion/react";
import facepaint from 'facepaint'
import { ThemeOptions } from "@mui/material";
import { blue } from "@mui/material/colors";

const mq = facepaint([
  '@media(min-width: 420px)',
  '@media(min-width: 920px)',
  '@media(min-width: 1320px)',
  '@media(min-width: 1420px)'
]);

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

export const formGroupWidth = css(mq({
  width: ['100%', '85%', '50%', '30%'],
}));

export const formGroup = css ({
    display: 'flex',
    flexDirection: 'column',
    marginBottom: '1%',
});

export const formGroupFullWidth = css ({
    display: 'flex',
    flexDirection: 'column',
    width: '100%',
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

export const navbarLink = css({
  color: '#ffffff',
  '&:after': {
    color: '#ffffff',
  }
});

export const marginRight2 = css({
  marginRight: '2%'
});

export const marginRight05 = css({
  marginRight: '0.5%'
});

export const marginLeft5 = css({
  marginLeft: 5
});

export const marginLeft1 = css({
  marginLeft: '1%'
});

export const marginLeft15 = css({
  marginLeft: '25%'
});

export const marginRight1 = css({
  marginRight: '1%'
});

export const marginTop2dot5 = css({
  marginTop: '2.5%',
});

export const marginBottom2 = css({
  marginBottom: '2%'
});

export const marginBottom3dot8 = css({
  marginBottom: '3.8%'
});

export const createRouteHeader = css({
  marginBottom: '0.4%'
});

export const createRouteSubHeadings = css({
  marginTop: '3%'
});

export const margin2 = css({
  margin: '2%'
});

export const margin3 = css({
  margin: '3%'
});

export const margin1dot5 = css({
  margin: '1.5%'
});

export const paddingBottom2 = css({
  paddingBottom: '2%'
});

export const padding2 = css({
  padding: '2%'
});


export const paddingTop05 = css({
  paddingTop: '0.5%'
});

export const map = css({
  height: '80vh',
  width: '100%',
  marginTop: '1%'
});

export const createMap = css({
  height: '70vh',
  width: '100%',
  marginTop: '1%'
});

export const viewMap = css({
  height: '70vh',
  width: '100%',
  marginTop: '1%'
});

export const createNewMapButton = css({
  display: 'flex',
  alignItems: 'flex-end',
  justifyContent: 'end',
  marginBottom: 'auto',
  flexBasis: '100%',
  flexDirection: 'column-reverse',
});

export const routeInfo = css({
  display: 'flex',
  alignItems: 'start',
  justifyContent: 'start',
  marginBottom: '2%',
  flexBasis: '100%',
  flexDirection: 'column'
});

export const noMargins = css({
  margin: '0'
});

export const flex = css({
  display: 'flex'
})

export const dashboardRightSide = css({
  margin: '1.5%'
});

export const displayInlineFlex = css({
  display: 'inline-flex'
});

export const highlightedBox = css({
  borderColor: 'blue',
  borderWidth: '12px'
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