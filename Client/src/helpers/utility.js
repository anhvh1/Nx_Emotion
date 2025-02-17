export const emptyCache = () => {
  if (process.env.NODE_ENV === "production") {
    if ("caches" in window) {
      caches.keys().then((names) => {
        // Delete all the cache files
        names.forEach((name) => {
          caches.delete(name);
        });
      });
    }
  }
};
