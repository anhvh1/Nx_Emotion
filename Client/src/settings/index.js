const date = new Date();
const currentYear = date.getFullYear();
export default {
  v2Url: "https://displaycms.gosol.com.vn/api/v2/", // api
};
const siteConfig = {
  siteName: "SMARTSIGNAGE",
  siteIcon: "", //ion-flash
  footerText: `Copyright © ${currentYear} GO SOLUTIONS. All rights`,
};

const themeConfig = {
  topbar: "theme6",
  sidebar: "theme8",
  layout: "theme2",
  theme: "themedefault",
};

export { siteConfig, themeConfig };
