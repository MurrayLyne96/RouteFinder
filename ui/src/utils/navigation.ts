const replacePathNavigation = (route: string, value: any) => {
    const linkParameter = route.substring(0, route.indexOf(":"));
    return `${linkParameter}${value}`;
};

const NavigationUtils = {
    replacePathNavigation,
};

export default NavigationUtils;
