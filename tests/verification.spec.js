const { test, expect } = require('@playwright/test');

test('homepage screenshot', async ({ page }) => {
  await page.goto('/');
  await page.screenshot({ path: '/home/jules/verification/homepage.png', fullPage: true });
});
